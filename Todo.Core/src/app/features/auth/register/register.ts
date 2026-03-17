import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Auth } from '../../../core/services/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
  standalone:true
})
export class Register {
  registerForm: any | FormGroup;
  isLoading = false;
  errorMessage = '';
  constructor(
    private fb: FormBuilder,
    private authservice: Auth,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      FirstName: ['', [Validators.required, Validators.minLength(3)]],
      LastName: ['', [Validators.required, Validators.minLength(3)]],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(6)]],
      PhoneNo: ['', [Validators.required, Validators.minLength(9)]],
      Gender:['',[Validators.required]]
    });
  }
  genders: string[] = ['Male', 'Female', 'Other'];
  onSubmit() {
    if (this.registerForm.invalid) return;
    this.isLoading = true;
    this.authservice.register(this.registerForm.value).subscribe({
      next: (res) => {
        if(res.statuscode == 102){
          this.isLoading = false;
          this.router.navigate(['/login']);
        }
      },
      error: (err) => {
        this.errorMessage = 'Registration Failed';
        this.isLoading = false;
      }
    })
  }
}
