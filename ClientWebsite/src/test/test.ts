import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test',
  imports: [],
  templateUrl: './test.html',
  styleUrl: './test.scss'
})
export class Test implements OnInit {
  test: string = "initial";

  setTest() {
    console.log("SETTING TEST");
    this.test = "changed";
  }

  ngOnInit(): void {
    setTimeout(() => this.setTest(), 2000);
  }
}
