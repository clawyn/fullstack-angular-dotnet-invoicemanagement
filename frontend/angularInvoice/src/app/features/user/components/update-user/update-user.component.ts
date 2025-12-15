import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {UserService} from '../../services/user.service';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {UpdateUserRequestModel} from '../../models/update-user-request.model';
import {Button} from 'primeng/button';
import {InputText} from 'primeng/inputtext';
import {MessagesModule} from 'primeng/messages';
import {Message} from 'primeng/message';

@Component({
  selector: 'app-update-user',
  imports: [
    ReactiveFormsModule,
    InputText,
    Button,
    MessagesModule,
    Message
  ],
  templateUrl: './update-user.component.html',
  styleUrl: './update-user.component.scss'
})
export class UpdateUserComponent {

  private readonly userService: UserService = inject(UserService);
  private readonly _fb: FormBuilder = inject(FormBuilder);

  @Input()
  userEmail?: string;
  @Output()
  close = new EventEmitter<void>();

  updateForm: FormGroup;
  errorMsg: string = '';

  constructor() {
    this.updateForm = this._fb.group({
      login: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  submitForm() {
    if (this.updateForm.invalid) return;

    if (this.userEmail) {
      this.updateUser();
    } else {
      this.addUser();
    }
  }

  addUser(){
    if (this.updateForm.invalid) return;

    this.userService.addUser(this.updateForm.value).subscribe({
      next: () => {
        this.close.emit();
        this.errorMsg = "Add successful"
      },

      error: err => {
        console.log(err);
        this.errorMsg = err.error;
      }
    })
  }

  updateUser() {
    if (this.updateForm.invalid) return;

    const updatedUser: UpdateUserRequestModel = this.updateForm.value;

    this.userService.updateUser(this.userEmail!, updatedUser).subscribe({
      next: () => {
        console.log('User updated');
        this.close.emit();
      },
      error: err => {
        console.error('Error updated', err)
        this.errorMsg = err.error|| 'An error occurred during the update.';
      }
    });
  }
}

