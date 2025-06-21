import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TravelDataService } from '../../services/travel-data.service';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent {
  searchDestination = '';
  searchDate = '';
  searchGuests = '';

  constructor(
    private router: Router,
    private travelDataService: TravelDataService
  ) { }

  searchTrips() {
    const searchData = {
      destination: this.searchDestination,
      date: this.searchDate,
      guests: this.searchGuests
    };
    
    this.travelDataService.updateSearchData(searchData);
    
    // Navigate to destinations page with search results
    this.router.navigate(['/destinations'], { 
      queryParams: { search: this.searchDestination } 
    });
  }
}
