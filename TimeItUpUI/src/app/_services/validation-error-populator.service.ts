import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ValidationErrorPopulatorService {

  constructor(private toastr: ToastrService,) { }

  public async populateValidationErrorArray(err: any, reqErrors: string[]): Promise<string[]> {
    let validationErrorDictionary = err.error.errors;

    if (err.error.errors !== null) {
      for (var fieldName in err.error.errors) {
        if (!reqErrors.hasOwnProperty(fieldName)) {
          reqErrors.push(validationErrorDictionary[fieldName]);
        }
      }
      this.toastr.warning('The form contains incorrectly entered data');
    }

    return reqErrors;
  }
}
