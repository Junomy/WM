import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'WM.UI';
  loggedIn!: boolean;
  
  ngOnInit(): void {
    this.loggedIn = false;
  }

  onLoggedIn(isLoggedIn: boolean) {
    this.loggedIn = isLoggedIn;
  }
}
