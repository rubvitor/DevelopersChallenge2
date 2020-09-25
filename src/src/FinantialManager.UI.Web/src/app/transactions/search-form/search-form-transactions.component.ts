import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { OFXService } from 'app/services/ofx.service';
import { STMTTRNModel } from 'app/models/STMTTRNModel';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-search-form-transactions',
  templateUrl: './search-form-transactions.component.html',
  styleUrls: ['./search-form-transactions.component.css']
})
export class SearchFormTransactionsComponent implements OnInit {

  private transactionForm: FormGroup;

  @Output() transactionsResponse = new EventEmitter();

  constructor(private ofxService: OFXService) { }

  ngOnInit() {
    this.transactionForm = new FormGroup({
      bankId: new FormControl('bankId', Validators.required),
      account: new FormControl('account', Validators.required),
    });
  }

  getTransactionByAccount(event: Event) {
    if (this.transactionForm.invalid) {
      return;
    }

    const bankId = this.transactionForm.get('bankId').value;
    const account = this.transactionForm.get('account').value;
    this.ofxService.getTransactionsByAccount(bankId, account)
        .subscribe(res => {
            this.transactionsResponse.emit(res);
      });
  }
}
