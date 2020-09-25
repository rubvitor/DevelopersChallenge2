import { Injectable } from '@angular/core';
import { HttpService } from 'app/core/http-services.service';
import { STMTTRNModel } from 'app/models/STMTTRNModel';
import { ValidationResultModel } from 'app/models/validation-result';
import { environment } from 'environments/environment';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from 'app/core/config/config.service';

@Injectable({
  providedIn: 'root',
})
export class OFXService extends HttpService {

  constructor(http: HttpClient,
              config: ConfigService) {
    super(http, config);
  }

  addOFX(files: any) {
    const url = `${environment.baseApi}/ofx-management`;
    return super.post<ValidationResultModel[]>(url, files);
  }

  getTransactionsByAccount(bankId: number, account: string) {
    const url = `${environment.baseApi}/ofx-management/transactions/${bankId}/${account}`;
    return super.get<STMTTRNModel[]>(url);
  }
}