import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../_services';

@Component({
  selector: 'app-logout-user-modal',
  templateUrl: './logout-user-modal.component.html',
  styleUrls: ['./logout-user-modal.component.scss']
})
export class LogoutUserModalComponent {

  @Input() fromParent: any;

  constructor(private modalService: NgbModal,
    private authService: AuthService,
    private router: Router,
    public activeModal: NgbActiveModal  ) { }

  ngOnInit() {
    console.log(this.fromParent);
    /* Output:
     {prop1: "Some Data", prop2: "From Parent Component", prop3: "This Can be anything"}
    */
  }

  closeModal(sendData: any) {
    this.activeModal.close(sendData);
  }
}
