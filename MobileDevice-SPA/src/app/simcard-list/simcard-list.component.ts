import { PaginatedResult } from './../_models/pagination';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { SimcardService } from './../_services/simcard.service';
import { Simcard } from './../_models/simcard';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-simcard-list',
  templateUrl: './simcard-list.component.html',
  styleUrls: ['./simcard-list.component.css']
})
export class SimcardListComponent implements OnInit {

  list: Simcard[];
  pagination: Pagination;
  filter: any = {
    iccid: '',
    phoneNumber: '',
    carrier: ''
  };
  status = 'Active';
  elementLabel = 'sim card';
  pageLabel = 'Sim Cards';
  pageRoute = 'simcards';
  maxSize = 10;

  carriers = ['AT&T', 'Verizon'];

  constructor(private service: SimcardService, private alertify: AlertifyService,
                private router: Router, private route: ActivatedRoute ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.list = data['simcards'].result;
      this.pagination = data['simcards'].pagination;
    });
  }

  loadList() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }
    this.service.getSimcards(this.pagination.currentPage, this.pagination.itemsPerPage,
        this.filter, activeStatus)
        .subscribe((res: PaginatedResult<Simcard[]>) => {
          this.list = res.result;
          this.pagination = res.pagination;
        });
  }

  filterTable() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
    this.loadList();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadList();
  }

  deactivateElement(id: number) {
    this.alertify.confirm('Are you sure you want to delete this ' + this.elementLabel + '?', () => {
      this.service.deactivateSimcard(id)
        .subscribe(() => {
          this.loadList();
        }, error => {
          this.alertify.error('Failed to delete ' + this.elementLabel);
        });
    });
  }

  clearFilter() {
    this.filter.iccid = '';
    this.filter.phoneNumber = '';
    this.filter.carrier = '';
    this.filterTable();
  }

  filterByCarrier(carrier: string) {
    this.filter.carrier = carrier;
    this.filterTable();
  }

  phoneIdentifier(element: Simcard) {
    const ln = element.assigneeLastName;
    const fn = element.assigneeFirstName;
    let name = '';
    if (ln == null || fn == null) {
      name = 'unassigned';
    } else {
      name = ln + ', ' + fn;
    }
    return name + ' - ' + (element.productModelName == null ? 'unknown' : element.productModelName);
  }
}
