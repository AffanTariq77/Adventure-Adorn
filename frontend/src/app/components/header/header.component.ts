import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { TravelDataService } from '../../services/travel-data.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  isMenuOpen = false;
  showSearch = false;
  searchDestination = '';
  searchDate = '';
  searchGuests = '';

  @Output() openLoginModal = new EventEmitter<void>();
  @Output() openSignupModal = new EventEmitter<void>();

  constructor(
    private router: Router,
    private travelDataService: TravelDataService
  ) { }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  toggleSearch() {
    this.showSearch = !this.showSearch;
  }

  closeSearch() {
    this.showSearch = false;
  }

  openLogin() {
    this.openLoginModal.emit();
  }

  openSignup() {
    this.openSignupModal.emit();
  }

  performSearch() {
    const searchData = {
      destination: this.searchDestination,
      date: this.searchDate,
      guests: this.searchGuests
    };
    
    this.travelDataService.updateSearchData(searchData);
    this.closeSearch();
    
    // Navigate to destinations page with search results
    this.router.navigate(['/destinations'], { 
      queryParams: { search: this.searchDestination } 
    });
  }
}
