import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
  email = '';

  subscribeNewsletter() {
    if (this.email && this.email.includes('@')) {
      // Here you would typically send the email to your backend
      console.log('Newsletter subscription:', this.email);
      alert('Thank you for subscribing to our newsletter!');
      this.email = '';
    } else {
      alert('Please enter a valid email address.');
    }
  }
}
