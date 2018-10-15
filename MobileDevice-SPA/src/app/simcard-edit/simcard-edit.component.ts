import { AlertifyService } from './../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Simcard } from '../_models/simcard';
import { SimcardService } from '../_services/simcard.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-simcard-edit',
  templateUrl: './simcard-edit.component.html',
  styleUrls: ['./simcard-edit.component.css']
})
export class SimcardEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  element: Simcard;
  newElement: Simcard = {
    id: 0,
    iccid: '',
    phoneNumber: '',
    carrier: '',
    active: true,
    deviceId: 0,
    assigneeFirstName: '',
    assigneeLastName: '',
    productModelName: ''
  };
  elementLabel = 'Sim Card';
  elementTypeLabel = 'sim card';
  elementRoute = 'simcards';
  originalElement: Simcard;

  formInvalid = true;

  carrierList: string[] = ['AT&T', 'Verizon'];

  constructor(private service: SimcardService, private router: Router,
                private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.element = this.newElement;
    this.originalElement = this.newElement;
    // this.carrierList = new Carriers();

    this.route.data.subscribe(data => {
      if (data['simcard']) {
        this.element = data['simcard'];
        this.isFormValid();
        this.populateOriginal();
      }
    });
  }

  updateElement() {
    this.service.updateSimcard(this.element)
      .subscribe(next => {
        this.editForm.reset(this.element);
        this.alertify.success(this.elementLabel + ' updated successfully');
      }, error => {
        this.alertify.error(error);
      });
  }

  insertElement() {
    this.service.addSimcard(this.element)
      .subscribe((element: Simcard) => {
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
      this.service.deactivateSimcard(id)
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


  markAsDirty() {
    this.editForm.control.markAsDirty();
    this.isFormValid();
  }

  isFormValid() {
    this.formInvalid = false;
    if (this.element.iccid === '' || this.element.phoneNumber === '' || this.element.carrier === '') {
      this.formInvalid = true;
    }
  }

  populateOriginal() {
    this.originalElement.id = this.element.id;
    this.originalElement.iccid = this.element.iccid;
    this.originalElement.active = this.element.active;
  }

}
