import { Component, OnInit, ViewChild } from '@angular/core';
import { faPlusCircle, faUserEdit, faWindowClose } from '@fortawesome/free-solid-svg-icons';
import { LogoutUserModalComponent } from '../logout-user-modal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faWindowClose = faWindowClose;
  faUserEdit = faUserEdit;

  @ViewChild(LogoutUserModalComponent)
  logoutUserModalComponent!: LogoutUserModalComponent;

  constructor(
    private modalService: NgbModal,
  ) {}

  ngOnInit(): void {
  }

  openLogoutModal() {
    const modalRef = this.modalService.open(LogoutUserModalComponent,
      {
        scrollable: true,
        windowClass: 'myCustomModalClass',
        keyboard: false,
        backdrop: 'static'
      });

    let data = {
      prop1: 'Some Data',
      prop2: 'From Parent Component',
      prop3: 'This Can be anything'
    }

    modalRef.componentInstance.fromParent = data;
    modalRef.result.then((result) => {
      console.log(result);
    }, (reason) => {
    });
  }
}
