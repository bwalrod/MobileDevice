import { ProductModel } from './../_models/productmodel';
import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductmodelService } from '../_services/productmodel.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-productmodel-edit',
  templateUrl: './productmodel-edit.component.html',
  styleUrls: ['./productmodel-edit.component.css']
})
export class ProductmodelEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: ProductModel;
  newElement: ProductModel = {
    id: 0,
    name: '',
    active: true,
    productTypeId: 1,
    productTypeName: '',
    productManufacturerId: 1,
    productManufacturerName: '',
    productCount: 0
  };
  elementLabel = 'Product Model';
  elementTypeLabel = 'product model';
  elementRoute = 'productmodels';
  originalElement: ProductModel;

  constructor(private service: ProductmodelService, private router: Router,
              private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      if (data['productmodel']) {
        this.element = data['productmodel'];
        this.populateOriginal();
      }
    });
  }

  updateElement() {
    this.service.updateProductModel(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
      });
  }

  insertElement() {
    this.service.addProductModel(this.element)
      .subscribe((element: ProductModel) => {
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

  deactivateElement(id: number, e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementTypeLabel + '?', () => {
      this.service.deativateProductModel(id)
        .subscribe(() => {
          this.router.navigate(['/' + this.elementRoute]);
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementTypeLabel);
        });
    });
  }

  returnToList(e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.router.navigate(['/' + this.elementRoute]);
  }

  setProductType(productType: number) {
    this.element.productTypeId = productType;
    this.markAsDirty();
  }

  setProductManufacturer(productManufacturer: number) {
    this.element.productManufacturerId = productManufacturer;
    this.markAsDirty();
  }

  markAsDirty() {
    this.editForm.control.markAsDirty();
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.name = this.element.name;
    this.originalElement.active = this.element.active;
  }
}
