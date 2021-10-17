import { Component, OnInit } from '@angular/core';
import { faPlusCircle, faUserEdit, faWindowClose } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faWindowClose = faWindowClose;
  faUserEdit = faUserEdit;


  constructor() { }

  ngOnInit(): void {
  }

}
