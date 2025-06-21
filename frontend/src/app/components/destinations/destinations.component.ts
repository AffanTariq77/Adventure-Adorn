import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TravelDataService, Destination } from '../../services/travel-data.service';

@Component({
  selector: 'app-destinations',
  templateUrl: './destinations.component.html',
  styleUrls: ['./destinations.component.scss']
})
export class DestinationsComponent implements OnInit {
  destinations: Destination[] = [];
  filteredDestinations: Destination[] = [];
  searchQuery = '';
  selectedDifficulty = '';
  selectedLocation = '';

  constructor(
    private travelDataService: TravelDataService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.destinations = this.travelDataService.getDestinations();
    this.filteredDestinations = [...this.destinations];

    // Check for search query from URL
    this.route.queryParams.subscribe(params => {
      if (params['search']) {
        this.searchQuery = params['search'];
        this.filterDestinations();
      }
    });
  }

  filterDestinations() {
    this.filteredDestinations = this.destinations.filter(destination => {
      const matchesSearch = !this.searchQuery || 
        destination.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        destination.location.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        destination.description.toLowerCase().includes(this.searchQuery.toLowerCase());

      const matchesDifficulty = !this.selectedDifficulty || 
        destination.difficulty === this.selectedDifficulty;

      const matchesLocation = !this.selectedLocation || 
        destination.location === this.selectedLocation;

      return matchesSearch && matchesDifficulty && matchesLocation;
    });
  }

  resetFilters() {
    this.searchQuery = '';
    this.selectedDifficulty = '';
    this.selectedLocation = '';
    this.filteredDestinations = [...this.destinations];
  }

  exploreDestination(destination: Destination) {
    console.log('Exploring destination:', destination.name);
    // Here you would typically navigate to a detailed view
    // this.router.navigate(['/destination', destination.id]);
  }

  bookDestination(destination: Destination) {
    console.log('Booking destination:', destination.name);
    // Here you would typically open a booking modal or navigate to booking page
    // this.router.navigate(['/booking', destination.id]);
  }
}
