import { Component, EventEmitter, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-logout-user-modal',
  templateUrl: './logout-user-modal.component.html',
  styleUrls: ['./logout-user-modal.component.scss']
})
export class LogoutUserModalComponent {

  @Output() logoutUserEvent = new EventEmitter<string>();

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {

  }

  closeModal() {
    this.activeModal.close();
  }

  logoutUser() {
    this.logoutUserEvent.emit("");
    this.closeModal();
  }
}
