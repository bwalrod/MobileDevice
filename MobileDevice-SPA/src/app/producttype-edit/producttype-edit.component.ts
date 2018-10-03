import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductType } from '../_models/ProductType';
import { ProductTypeService } from '../_services/producttype.service';

@Component({
  selector: 'app-producttype-edit',
  templateUrl: './producttype-edit.component.html',
  styleUrls: ['./producttype-edit.component.css']
})
export class ProducttypeEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: ProductType;
  newElement: ProductType = {
    id: 0,
    name: '',
    active: true,
    productModelCount: 0
  };
  elementLabel = 'Product Type';
  elementTypeLabel = 'product type';
  elementRoute = 'producttypes';


  constructor(private service: ProductTypeService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;

    this.route.data.subscribe(data => {
      if (data['producttype']) {
        this.element = data['producttype'];
      }
    });
  }

  updateElement() {
    this.service.updateProductType(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
      });
  }

  insertElement() {
    this.service.addProductType(this.element)
      .subscribe((element: ProductType) => {
        this.alertify.success(this.elementLabel + ' added successfully');
        this.element = element;
      }, error => {
        this.alertify.error(error);
      });
  }

  submitForm() {
    if (this.editForm.dirty) {
      if (this.element.id > 0) {
        this.updateElement();
      } else {
        this.insertElement();
      }
    }
  }

  deactivateElement(id: number) {
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementTypeLabel + '?', () => {
      this.service.deactivateProductType(id)
        .subscribe(() => {
          this.router.navigate(['/producttypes']);
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementTypeLabel);
        });
    });
  }

  returnToList() {
    this.router.navigate(['/producttypes']);
  }
}
