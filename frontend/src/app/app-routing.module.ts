import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Component imports
import { HeroComponent } from './components/hero/hero.component';
import { FeaturesComponent } from './components/features/features.component';
import { DestinationsComponent } from './components/destinations/destinations.component';
import { HotelsComponent } from './components/hotels/hotels.component';
import { CarRentalComponent } from './components/car-rental/car-rental.component';
import { CommunityComponent } from './components/community/community.component';
import { AboutComponent } from './components/about/about.component';
import { ContactComponent } from './components/contact/contact.component';
import { LoginComponent } from './components/auth/login/login.component';
import { SignupComponent } from './components/auth/signup/signup.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HeroComponent },
  { path: 'destinations', component: DestinationsComponent },
  { path: 'hotels', component: HotelsComponent },
  { path: 'car-rental', component: CarRentalComponent },
  { path: 'community', component: CommunityComponent },
  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: '**', redirectTo: '/home' } // Catch all route
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
