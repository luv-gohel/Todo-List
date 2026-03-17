import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm: any | FormGroup;
  isSubmitting = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private authservice: Auth,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  submit():void{
    if(this.loginForm.invalid){
      this.loginForm.markAllAsTouched();
      return;
    }
    this.isSubmitting=true;
    this.errorMessage='';
    this.authservice.login(this.loginForm.value).subscribe(
      {
        next:(res:any)=>{
          this.isSubmitting = false;
          console.log(res);
          if(res.statuscode){
            this.authservice.setUserGuid(res.userid)
            // this.router.navigate(['/dashboard']);
            // console.log("Login SuccessFull");
            this.router.navigate(['/dashboard']);
          }
          else{
            this.errorMessage=res.message;
          }
          
        },
        error:()=>{
          this.isSubmitting=false;
          this.errorMessage='Something Went Wrong. Please Try Again';
        }
      }
    )
  }

}
