import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { AvatarModule } from 'primeng/avatar';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterModule,
    MenubarModule,
    AvatarModule,
    InputTextModule,
    RippleModule
  ],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class NavbarComponent implements OnInit {

  items: MenuItem[] = [];

  ngOnInit(): void {
    this.items = [
      {
        label: 'Dashboard',
        icon: 'pi pi-chart-bar',
        routerLink: '/dashboard'
      },
      {
        label: 'Doctors',
        icon: 'pi pi-users',
        items: [
          {
            label: 'Doctors',
            icon: 'pi pi-user',
            routerLink: '/doctors'
          },
          {
            label: 'Specialty',
            icon: 'pi pi-list',
            routerLink: '/specialty'
          },
          {
            label: 'Doctor Schedule',
            icon: 'pi pi-calendar',
            routerLink: '/doctor-schedule'
          }
        ]
      },
      {
        label: 'Patients',
        icon: 'pi pi-user',
        routerLink: '/patients'
      },
      {
        label: 'Medical Services',
        icon: 'pi pi-heart',
        routerLink: '/medical-services'
      },
      {
        label: 'Clinic',
        icon: 'pi pi-building',
        items: [
          {
            label: 'Clinic',
            icon: 'pi pi-building',
            routerLink: '/clinc'
          },
          {
            label: 'Appointment',
            icon: 'pi pi-calendar-plus',
            routerLink: '/appointment'
          }
        ]
      }
    ];
  }
}
