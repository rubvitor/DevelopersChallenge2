import { Component, OnInit } from '@angular/core';
import { OFXService } from 'app/services/ofx.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ofx-import',
  templateUrl: './ofx-import.component.html',
  styleUrls: ['./ofx-import.component.css']
})
export class OFXImportComponent implements OnInit {

  inputFiles: any[];

  constructor(private ofxService: OFXService,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.inputFiles = [];
  }

  enviarOFX() {
    const formData = new FormData();
    if (this.inputFiles.length > 0) {
    for (let i = 0; i < this.inputFiles.length; i++) {
      formData.append(`files`, this.inputFiles[i]);
    }

    this.ofxService.addOFX(formData).subscribe(data => {
        if (data && data.length > 0) {
          data.forEach(i => {
            if (!i.isValid) {
              this.toastr.error(i.errors[0].errorMessage);
            } else {
              this.toastr.success('Arquivo importado com sucesso.');
            }
          });
        } else {
          this.toastr.error('Os arquivos OFX não puderem ser importados.');
        }
    });
  }
  }

  onFileChange(event) {
      this.inputFiles = event.target.files;
  }
}
