import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Manufacturer } from '../_models/manufacturer';
import { ManufacturerService } from '../_services/manufacturer.service';

@Component({
  selector: 'app-manufacturer-edit',
  templateUrl: './manufacturer-edit.component.html',
  styleUrls: ['./manufacturer-edit.component.css']
})
export class ManufacturerEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  manufacturer: Manufacturer;
  newManufacturer: Manufacturer = {
    id: 0,
    name: '',
    active: true,
    productCount: 0
  };

  constructor(private manuService: ManufacturerService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.manufacturer = this.newManufacturer;

    this.route.data.subscribe(data => {
      if (data['manufacturer']) {
        this.manufacturer = data['manufacturer'];
      }
    });
  }

  updateManufacturer() {
    this.manuService.updateManufacturer(this.manufacturer)
      .subscribe(next => {
        this.editForm.reset(this.manufacturer);
        this.alertify.success('Manufacturer updated successfully');
      }, error => {
        this.alertify.error(error);
      });
  }

  insertManufacturer() {
    this.manuService.addManufacturer(this.manufacturer)
      .subscribe((manufacturer: Manufacturer) => {
        this.alertify.success('Manufacturer added successfully');
        this.manufacturer = manufacturer;
        this.editForm.reset(this.manufacturer);
        this.router.navigate(['/manufacturers/edit/' + this.manufacturer.id]);
      }, error => {
        this.alertify.error(error);
      });
  }

  submitForm() {
    if (this.editForm.dirty) {
      if (this.manufacturer.id > 0) {
        this.updateManufacturer();
      } else {
        this.insertManufacturer();
      }
    }
  }

  deactivateManufacturer(id: number) {
    this.alertify.confirm('Are you sure you want to delete this manufacturer?', () => {
      this.manuService.deactivateManufacturer(id)
        .subscribe(() => {
          this.router.navigate(['/manufacturers']);
        }, error => {
          this.alertify.error('Failed to delete manufacturer');
        });
    });
  }

  returnToList() {
    this.router.navigate(['/manufacturers']);
  }
}
