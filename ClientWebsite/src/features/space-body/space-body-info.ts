import { AfterViewInit, ChangeDetectorRef, Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatTree, MatTreeModule } from '@angular/material/tree';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SpacebodyService } from '../../services/spacebody-service';
import { SpaceBody, SpaceBodyType } from '../../types/SpaceBody';
import { AstronomerService } from '../../services/astronomer-service';
import { Astronomer, AstronomerOccupation } from '../../types/Astronomer';

@Component({
  selector: 'app-space-body',
  imports: [MatChipsModule, MatTreeModule, MatIconModule, MatButtonModule, RouterLink],
  templateUrl: './space-body-info.html',
  styleUrl: './space-body-info.scss'
})
export class SpaceBodyInfo implements OnInit {
  private route = inject(ActivatedRoute);
  private spacebodyService = inject(SpacebodyService);
  private astronomerService = inject(AstronomerService);
  private changeDetectorRef = inject(ChangeDetectorRef);

  @ViewChild('tree',  { static: false }) tree!: MatTree<SpaceBody>;

  treeDataSource = signal<SpaceBody[]>([]);

  SpaceBodyType = SpaceBodyType;
  AstronomerOccupation = AstronomerOccupation;

  currentBody = signal<SpaceBody | undefined>(null!);
  bodyAstronomer = signal<Astronomer | null>(null!);

  childrenAccessor = (node: SpaceBody) => node.children ?? [];
  hasChild = (_: number, node: SpaceBody) => !!node.children && node.children.length > 0;
  
  ngOnInit(): void {

    this.route.paramMap.subscribe(() => {
      this.getCurrentBody();
    });
    
  }

  getCurrentBody() {
    this.spacebodyService.getBody(Number(this.route.snapshot.paramMap.get('id'))).subscribe({
      next: result => {
        if(result == null) return;
        this.currentBody.set(result);
        if(this.currentBody()?.discovererId != null) this.getAstronomer();
        this.getSpaceBodyHierarchy();
      }
    })
  }

  getAstronomer() {
    this.astronomerService.getAstronomer(this.currentBody()?.discovererId!).subscribe({
      next: result => {
        this.bodyAstronomer.set(result);
      }
    });
  }

  getSpaceBodyHierarchy() {
    this.spacebodyService.getBodyHierarchy(this.currentBody()?.id!).subscribe({
      next: result => {
        this.treeDataSource.set([result]);
        this.changeDetectorRef.detectChanges();
        this.tree.expandAll();
      }
    })
  }
}
