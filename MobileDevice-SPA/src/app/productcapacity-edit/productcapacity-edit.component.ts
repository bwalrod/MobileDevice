import { AlertifyService } from './../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductCapacityService } from './../_services/productcapacity.service';
import { ProductCapacity } from './../_models/productcapacity';
import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-productcapacity-edit',
  templateUrl: './productcapacity-edit.component.html',
  styleUrls: ['./productcapacity-edit.component.css']
})
export class ProductcapacityEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: ProductCapacity;
  newElement: ProductCapacity = {
    id: 0,
    name: '',
    active: true,
    productTypeId: 100,
    productTypeName: '',
    productManufacturerId: 100,
    productManufacturerName: '',
    productModelId: 100,
    productModelName: '',
    productCount: 0
  };
  elementLabel = 'Product Capacity';
  elementTypeLabel = 'product capacity';
  elementRoute = 'productcapacities';
  originalElement: ProductCapacity;

  formInvalid = true;

  constructor(private service: ProductCapacityService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      if (data['productcapacity']) {
        this.element = data['productcapacity'];
        this.isFormValid();
        this.populateOriginal();
      }
    });
  }

  updateElement() {
    this.service.updateProductCapacity(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
      });
  }

  insertElement() {
    this.service.addProductCapacity(this.element)
      .subscribe((element: ProductCapacity) => {
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
      this.service.deativateProductCapacity(id)
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
    this.element.productModelId = null;
    this.markAsDirty();
  }

  setProductManufacturer(productManufacturer: number) {
    this.element.productManufacturerId = productManufacturer;
    this.element.productModelId = null;
    this.markAsDirty();
  }

  setProductModel(productModel: number) {
    this.element.productModelId = productModel;
    this.markAsDirty();
  }

  markAsDirty() {
    this.editForm.control.markAsDirty();
    this.isFormValid();
  }

  isFormValid() {
    this.formInvalid = false;
    if (this.element.productModelId == null) {
      this.formInvalid = true;
    }
    if (this.element.name === '') {
      this.formInvalid = true;
    }
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.name = this.element.name;
    this.originalElement.active = this.element.active;
  }
}
