import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-devicedate-add',
  templateUrl: './devicedate-add.component.html',
  styleUrls: ['./devicedate-add.component.css']
})
export class DevicedateAddComponent implements OnInit {
  @Input() deviceId: number;
  selectedDate: Date;


  constructor() { }

  ngOnInit() {
  }

}
