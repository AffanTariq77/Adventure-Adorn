import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Adventure Adorn';
  
  // Modal state
  showLoginModal = false;
  showSignupModal = false;
  
  // Form data
  loginForm = {
    email: '',
    password: ''
  };
  
  signupForm = {
    fullName: '',
    email: '',
    password: '',
    confirmPassword: ''
  };

  // Modal methods
  openLogin() {
    this.showLoginModal = true;
    this.showSignupModal = false;
  }

  closeLogin() {
    this.showLoginModal = false;
    this.resetLoginForm();
  }

  openSignup() {
    this.showSignupModal = true;
    this.showLoginModal = false;
  }

  closeSignup() {
    this.showSignupModal = false;
    this.resetSignupForm();
  }

  // Form submission methods
  onLoginSubmit() {
    if (this.loginForm.email && this.loginForm.password) {
      console.log('Login attempt:', this.loginForm);
      // Here you would typically call your authentication service
      // this.authService.login(this.loginForm.email, this.loginForm.password);
      this.closeLogin();
    }
  }

  onSignupSubmit() {
    if (this.signupForm.password === this.signupForm.confirmPassword) {
      console.log('Signup attempt:', this.signupForm);
      // Here you would typically call your authentication service
      // this.authService.signup(this.signupForm);
      this.closeSignup();
    } else {
      alert('Passwords do not match!');
    }
  }

  // Form reset methods
  private resetLoginForm() {
    this.loginForm = {
      email: '',
      password: ''
    };
  }

  private resetSignupForm() {
    this.signupForm = {
      fullName: '',
      email: '',
      password: '',
      confirmPassword: ''
    };
  }
}
