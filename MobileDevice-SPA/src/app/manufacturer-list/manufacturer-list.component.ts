import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from './../_services/alertify.service';
import { Pagination, PaginatedResult } from './../_models/pagination';
import { Manufacturer } from './../_models/manufacturer';
import { Component, OnInit } from '@angular/core';
import { ManufacturerService } from '../_services/manufacturer.service';

@Component({
  selector: 'app-manufacturer-list',
  templateUrl: './manufacturer-list.component.html',
  styleUrls: ['./manufacturer-list.component.css']
})
export class ManufacturerListComponent implements OnInit {

  manufacturers: Manufacturer[];
  pagination: Pagination;
  filter = '';
  status = 'Active';

  constructor(private manuService: ManufacturerService, private alertify: AlertifyService
      , private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.manufacturers = data['manufacturers'].result;
      this.pagination = data['manufacturers'].pagination;
      this.filter = null;
    });
  }

  loadManufacturers() {
    let activeStatus = 2;
    if (this.status === 'Active') {
      activeStatus = 1;
    }
    if (this.status === 'Inactive') {
      activeStatus = 0;
    }

    this.manuService.getManufacturers(this.pagination.currentPage, this.pagination.itemsPerPage,
        this.filter, activeStatus)
        .subscribe((res: PaginatedResult<Manufacturer[]>) => {
          this.manufacturers = res.result;
          this.pagination = res.pagination;
        });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadManufacturers();
  }

  filterTable() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 5;
    this.loadManufacturers();
  }

  deactivateManufacturer(id: number) {
    this.alertify.confirm('Are you sure you want to delete this manufacturer?', () => {
      this.manuService.deactivateManufacturer(id)
      .subscribe(() => {
        // this.router.navigate(['/departments']);
        this.loadManufacturers();
      }, error => {
        this.alertify.error('Failed to delete manufacturer');
      });
    });
  }
}
