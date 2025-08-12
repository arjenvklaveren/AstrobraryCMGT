import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatTree, MatTreeModule } from '@angular/material/tree';

@Component({
  selector: 'app-space-body',
  imports: [MatChipsModule, MatTreeModule, MatIconModule, MatButtonModule],
  templateUrl: './space-body.html',
  styleUrl: './space-body.scss'
})
export class SpaceBody implements AfterViewInit {
  @ViewChild('tree') tree!: MatTree<FoodNode>;
  dataSource = EXAMPLE_DATA;

  childrenAccessor = (node: FoodNode) => node.children ?? [];

  treeControl = new NestedTreeControl<FoodNode>(node => node.children);

  hasChild = (_: number, node: FoodNode) => !!node.children && node.children.length > 0;
  
  ngAfterViewInit() {
    this.tree.expandAll();
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
