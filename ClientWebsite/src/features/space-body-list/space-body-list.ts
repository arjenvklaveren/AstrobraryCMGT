import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { FilterBar } from "../../components/filter-bar/filter-bar";
import { MatTreeModule } from '@angular/material/tree';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { SpacebodyService } from '../../services/spacebody-service';
import { SpaceBody, SpaceBodyType } from '../../types/SpaceBody';
import { RouterLink } from '@angular/router';
import { SpaceBodyFilterParams } from '../../types/FilterParams';
import { ViewToggleValue } from '../../types/ViewToggleValue';
import { ObjectDialogObjectType } from '../../types/ObjectDialogObjectType';
import { ObjectDialogViewType } from '../../types/ObjectDialogViewType';
import { MatDialog } from '@angular/material/dialog';
import { ObjectDialog } from '../../components/dialogs/object-dialog/object-dialog';
import { SpaceBodyDialogPartial } from '../../components/dialogs/partial-content/space-body-dialog-partial/space-body-dialog-partial';

@Component({
  selector: 'app-space-body-list',
  imports: [FilterBar, MatTreeModule, MatCardModule, MatIconModule, MatButtonModule, MatChipsModule, RouterLink],
  templateUrl: './space-body-list.html',
  styleUrl: './space-body-list.scss'
})
export class SpaceBodyList implements OnInit {
  spacebodyService = inject(SpacebodyService);
  cdr = inject(ChangeDetectorRef);
  dialog = inject(MatDialog);

  spaceBodies = signal<SpaceBody[]>([]);
  spaceBodiesHierarchy = signal<SpaceBody[]>([]);
  
  viewToggleValues = signal<ViewToggleValue[]>([
    {
      value: "Hierarchy",
      icon: "class",
      selected: true
    },
    {
      value: "List",
      icon: "theaters",
      selected: false
    },
     {
      value: "Card",
      icon: "segment",
      selected: false
    },
  ]);

  filterParams = new SpaceBodyFilterParams();
  SpaceBodyType = SpaceBodyType;
  ObjectDialogObjectType = ObjectDialogObjectType;
  ObjectDialogViewType = ObjectDialogViewType;

  childrenAccessor = (node: SpaceBody) => node.children ?? [];
  hasChild = (_: number, node: SpaceBody) => !!node.children && node.children.length > 0;

  ngOnInit(): void {
    this.getBodies();
  }

  getBodies() {
    this.spacebodyService.getBodies(this.filterParams).subscribe({
      next: result => {
        this.spaceBodies.set(result);
        this.transformDataToHierarchy();
      }
    });
  }

  transformDataToHierarchy() {
    this.spaceBodiesHierarchy.set(this.spaceBodies());
    var bodyHashMap = new Map<number, SpaceBody>();

    this.spaceBodiesHierarchy().forEach(p => {
      bodyHashMap.set(p.id!, { ...p, children: [] });
    });

    bodyHashMap.forEach(b => {
      if(b.parentId != null) {
        var parent = bodyHashMap.get(b.parentId);
        if(parent && !hasCircularDependency(b, new Set([b.id!]))) parent?.children.push(b);
        else b.parentId = null;
      }
    });

    function hasCircularDependency(body: SpaceBody, path: Set<number>): boolean {
      var parent = bodyHashMap.get(body.parentId!);
      if(path.has(parent!.id!)) return true;
      else path.add(parent!.id!);
      if(!parent?.parentId) return false;
      return hasCircularDependency(parent, path);
    }

    var outArray = Array.from(bodyHashMap.values()).filter(b => !b.parentId);
    this.spaceBodiesHierarchy.set(outArray);
  }

  openEditDialog(spaceBody: SpaceBody) {
    var initialParentId = spaceBody.parentId;
    this.dialog.open(ObjectDialog, {
      data: {
        component: SpaceBodyDialogPartial,
        inputObject: spaceBody,
        viewType: ObjectDialogViewType.Edit,
      },
      panelClass: 'object-dialog-container',
    })
    .afterClosed().subscribe((result) => {
      this.cdr.detectChanges();

      if(result?.inputIsDeleted!) {
        let removeIds = this.getAllLowerHierarchyIds(spaceBody.id!);
        this.spaceBodies.update(bodies =>
          bodies.filter(body => !removeIds.includes(body.id!))
        );
        this.transformDataToHierarchy();
      }

      if(result?.inputIsSubmitted!) {
          this.spaceBodies.update(bodies =>
            bodies.map(obj => obj.id === spaceBody.id ? spaceBody : obj)
          );
          if (spaceBody.parentId != initialParentId) {
            this.transformDataToHierarchy();
          }
      }
    });
  }

  getAllLowerHierarchyIds(currentId: number | null) : number[] {
    let allIds: number[] = [];
    allIds.push(currentId!);
    let children = this.spaceBodies().filter(body => body.parentId === currentId);

    children.forEach((child => {
      allIds.push(child.id!); 
      allIds.push(...this.getAllLowerHierarchyIds(child.id!));
    }));

    return allIds;
  }

  onAddNewSpaceBody(spaceBody: SpaceBody) {
    this.spaceBodies.update(bodies => [...bodies, spaceBody]);
    this.transformDataToHierarchy();
  }

  onFiltersChange() {
    this.getBodies();
  }
}
