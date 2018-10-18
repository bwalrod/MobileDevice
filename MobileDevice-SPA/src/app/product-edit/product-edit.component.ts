import { AlertifyService } from './../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from './../_services/product.service';
import { Product } from './../_models/product';
import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: Product;
  newElement: Product = {
    id: 0,
    partNum: '',
    active: true,
    productTypeId: 1,
    productTypeName: '',
    productManufacturerId: 1,
    productManufacturerName: '',
    productModelId: 0,
    productModelName: '',
    productCapacityId: 0,
    productCapacityName: '',
    deviceCount: 0
  };
  elementLabel = 'Product';
  elementTypeLabel = 'product';
  elementRoute = 'products';
  originalElement: Product;
  formInvalid = false;

  constructor(private service: ProductService, private router: Router,
              private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;

    this.route.data.subscribe(data => {
      if (data['product']) {
        this.element = data['product'];
        this.populateOriginal();
        console.log('OnInit');
      }
    });
  }

  updateElement() {
    this.service.updateProduct(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
        setTimeout(() => {
          this.alertify.confirm('Would you like to restore the original values?', () => {
            this.restoreOriginal();
          });
        }, 2000);
      });
  }

  insertElement() {
    this.service.addProduct(this.element)
      .subscribe((element: Product) => {
        this.alertify.success(this.elementLabel + ' added successfully');
        this.element = element;
      }, error => {
        this.alertify.error(error);
      });
  }

  submitForm() {
    if (this.editForm.dirty) {
      if (this.haveValuesChanged() || this.element.id === 0) {
        if (this.element.id > 0) {
          this.updateElement();
        } else {
          this.insertElement();
        }
      } else {
        this.alertify.error('You haven\'t changed anything');
        this.editForm.control.markAsPristine();
        this.editForm.control.markAsUntouched();
      }
    }
  }

  deactivateElement(id: number, e: Event) {
    e.stopPropagation();
    e.preventDefault();
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementTypeLabel + '?', () => {
      this.service.deativateProduct(id)
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

  resetSelectables() {
    this.element.productCapacityId = 0;
    this.element.productModelId = 0;
  }

  setProductType(productType: number) {
    this.element.productTypeId = productType;
    this.resetSelectables();
    this.markAsDirty();
    this.testFormValidity();
  }

  setProductManufacturer(productManufacturer: number) {
    this.element.productManufacturerId = productManufacturer;
    this.resetSelectables();
    this.markAsDirty();
    this.testFormValidity();
  }

  setProductModel(productModel: number) {
    this.element.productModelId = productModel;
    this.markAsDirty();
    this.testFormValidity();
    // console.log('setProductModel');
  }

  setProductCapacity(productCapacity: number) {
    this.element.productCapacityId = productCapacity;
    this.markAsDirty();
    this.testFormValidity();
  }

  markAsDirty() {
    this.editForm.control.markAsDirty();
  }

  isFormValid() {
    return this.element.productTypeId > 0 &&
      this.element.productCapacityId > 0 &&
      this.element.productManufacturerId > 0 &&
      this.element.productModelId > 0 &&
      this.element.partNum !== '';
  }

  testFormValidity() {
      this.formInvalid = !this.isFormValid();
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.partNum = this.element.partNum;
    this.originalElement.productCapacityId = this.element.productCapacityId;
    this.originalElement.productManufacturerId = this.element.productManufacturerId;
    this.originalElement.productTypeId = this.element.productTypeId;
    this.originalElement.productModelId = this.element.productModelId;
    this.originalElement.active = this.element.active;
    console.log('populateOriginal');
  }

  haveValuesChanged() {
    return this.originalElement.partNum !== this.element.partNum ||
      this.originalElement.productCapacityId !== this.element.productCapacityId ||
      this.originalElement.productManufacturerId !== this.element.productManufacturerId ||
      this.originalElement.productTypeId !== this.element.productTypeId ||
      this.originalElement.productModelId !== this.element.productModelId ||
      this.originalElement.active !== this.element.active;
  }

  restoreOriginal() {
    this.element.partNum = this.originalElement.partNum;
    this.element.productCapacityId = this.originalElement.productCapacityId;
    this.element.productManufacturerId = this.originalElement.productManufacturerId;
    this.element.productTypeId = this.originalElement.productTypeId;
    this.element.productModelId = this.originalElement.productModelId;
    this.element.active = this.originalElement.active;
  }
}
