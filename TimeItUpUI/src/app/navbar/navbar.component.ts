import { Component, OnInit, ViewChild } from '@angular/core';
import { faPlusCircle, faUserEdit, faWindowClose } from '@fortawesome/free-solid-svg-icons';
import { LogoutUserModalComponent } from '../logout-user-modal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../_services';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faWindowClose = faWindowClose;
  faUserEdit = faUserEdit;

  constructor(
    private authService: AuthService,
    private modalService: NgbModal,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  logoutUser() {
    this.authService.logout();
    this.toastr.success("You have been logged out");
    this.router.navigate(["/login"]);
  }

  openLogoutModal() {

    const modalRef = this.modalService.open(LogoutUserModalComponent,
      {
        scrollable: true,
        windowClass: 'myCustomModalClass',
        keyboard: false,
        backdrop: 'static',
        centered: false,
        size: "modal-lg"
      });
    modalRef.componentInstance.logoutUserEvent.subscribe(($e: any) => {
      this.logoutUser();
    });
  }
}
