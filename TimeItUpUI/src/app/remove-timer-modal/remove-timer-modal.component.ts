import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-remove-timer-modal',
  templateUrl: './remove-timer-modal.component.html',
  styleUrls: ['./remove-timer-modal.component.scss']
})
export class RemoveTimerModalComponent implements OnInit {
  @Output() deleteTimerEvent = new EventEmitter<string>();

  constructor(private modalService: NgbModal,
    public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  closeModal() {
    this.activeModal.close();
  }

  removeTimer() {
    this.deleteTimerEvent.emit("");
    this.closeModal();
  }
}
