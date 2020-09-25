import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { STMTTRNModel } from 'app/models/STMTTRNModel';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;

  dtOptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject();

  stmttrnModelList: STMTTRNModel[];
  constructor(private toastr: ToastrService) { }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      processing: true
    };
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next();
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  responseTransactions(stmttrnModelList: STMTTRNModel[]) {
    this.stmttrnModelList = stmttrnModelList;
    this.rerender();

    if (!stmttrnModelList || stmttrnModelList.length === 0) {
        this.toastr.info('Não existem transações para os filtros informados.');
    }
  }

  rerender(): void {
    if (!this.dtElement || !this.dtElement.dtInstance) {
      return;
    }

      this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
        dtInstance.destroy();
        this.dtTrigger.next();
      });
  }
}
