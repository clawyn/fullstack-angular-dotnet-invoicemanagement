import {Component, inject} from '@angular/core';
import {UserService} from '../../services/user.service';
import {UserDtoModel} from '../../models/all-user-result.model';
import {TableModule} from 'primeng/table';
import {Button} from 'primeng/button';
import {UpdateUserComponent} from '../../components/update-user/update-user.component';
import {Popover} from 'primeng/popover';

@Component({
  selector: 'app-user-index',
  imports: [
    TableModule,
    Button,
    UpdateUserComponent,
    Popover
  ],
  templateUrl: './user-index.component.html',
  styleUrl: './user-index.component.scss'
})
export class UserIndexComponent {

  private readonly _userService: UserService = inject(UserService);

  users!: UserDtoModel[];
  size!: number;
  maxPage!: number;
  page!: number;
  selectedUser: any = null;

  constructor() {
  }

  loadUsers(event: any) {
    const page = event.first / event.rows + 1;
    const size = event.rows;

    this._userService.getAll(page, size).subscribe({
      next: data => {
        this.users = data.results;
        this.maxPage = data.totalPages;
        this.size = size;
        this.page = page;
      },
      error: err => console.error('Error retrieving users', err)
    });
  }

  deleteUser(email: string) {
    this._userService.deleteUser(email).subscribe({
      next: data => {
        this.loadUsers({
          first: (this.page - 1) * this.size,
          rows: this.size
        });
      },
      error: err => {
        console.log(err);
      }
    })
  }

  onSelectedUser(user: any){
    this.selectedUser = user;
  }

  onClose(){
    this.selectedUser = null;
    this.loadUsers({
      first: (this.page - 1) * this.size,
      rows: this.size
    });
  }
}
