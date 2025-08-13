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

  @ViewChild('tree',  { static: false }) tree!: MatTree<FoodNode>;
  dataSource = EXAMPLE_DATA;

  SpaceBodyType = SpaceBodyType;
  AstronomerOccupation = AstronomerOccupation;

  currentBodyId = this.route.snapshot.paramMap.get('id');
  currentBody = signal<SpaceBody | undefined>(null!);
  bodyAstronomer = signal<Astronomer | null>(null!);

  childrenAccessor = (node: FoodNode) => node.children ?? [];
  hasChild = (_: number, node: FoodNode) => !!node.children && node.children.length > 0;
  
  ngOnInit(): void {
    this.getCurrentBody();
  }

  getCurrentBody() {
    this.spacebodyService.getBody(Number(this.currentBodyId)).subscribe({
      next: result => {
        if(result == null) return;
        this.currentBody.set(result);
        this.changeDetectorRef.detectChanges();
        this.tree.expandAll();
        if(this.currentBody()?.discovererId != null) this.getAstronomer();
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
}


interface FoodNode {
  name: string;
  children?: FoodNode[];
}

const EXAMPLE_DATA: FoodNode[] = [
  {
    name: 'Fruit',
    children: [{name: 'Apple'}, {name: 'Banana'}, {name: 'Fruit loops'}],
  },
  {
    name: 'Vegetables',
    children: [
      {
        name: 'Green',
        children: [{name: 'Broccoli'}, {name: 'Brussels sprouts'}],
      },
      {
        name: 'Orange',
        children: [{name: 'Pumpkins'}, {name: 'Carrots'}],
      },
    ],
  },
];
