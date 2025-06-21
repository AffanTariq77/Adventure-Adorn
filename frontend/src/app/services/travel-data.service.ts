import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface Destination {
  id: number;
  name: string;
  description: string;
  price: number;
  rating: number;
  image: string;
  location: string;
  duration: string;
  difficulty: string;
}

export interface Hotel {
  id: number;
  name: string;
  location: string;
  price: number;
  rating: number;
  badge: string;
  image: string;
  amenities: Array<{ name: string; icon: string }>;
  description: string;
}

export interface CarRental {
  id: number;
  name: string;
  type: string;
  price: number;
  rating: number;
  image: string;
  features: string[];
  transmission: string;
  seats: number;
}

export interface Testimonial {
  id: number;
  name: string;
  location: string;
  text: string;
  avatar: string;
  rating: number;
}

@Injectable({
  providedIn: 'root'
})
export class TravelDataService {
  private searchData = new BehaviorSubject<any>({});
  searchData$ = this.searchData.asObservable();

  // Popular destinations data
  popularDestinations: Destination[] = [
    {
      id: 1,
      name: 'Hunza Valley',
      description: 'Breathtaking mountain views and ancient forts',
      price: 299,
      rating: 4.8,
      image: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=400&h=300&fit=crop',
      location: 'Gilgit-Baltistan',
      duration: '5-7 days',
      difficulty: 'Easy'
    },
    {
      id: 2,
      name: 'Swat Valley',
      description: 'The Switzerland of Pakistan with lush green valleys',
      price: 199,
      rating: 4.6,
      image: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=400&h=300&fit=crop',
      location: 'Khyber Pakhtunkhwa',
      duration: '4-6 days',
      difficulty: 'Easy'
    },
    {
      id: 3,
      name: 'Skardu',
      description: 'Gateway to the world\'s highest peaks',
      price: 399,
      rating: 4.9,
      image: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=400&h=300&fit=crop',
      location: 'Gilgit-Baltistan',
      duration: '6-8 days',
      difficulty: 'Moderate'
    },
    {
      id: 4,
      name: 'Chitral',
      description: 'Remote beauty with unique culture and traditions',
      price: 249,
      rating: 4.7,
      image: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=400&h=300&fit=crop',
      location: 'Khyber Pakhtunkhwa',
      duration: '5-7 days',
      difficulty: 'Moderate'
    },
    {
      id: 5,
      name: 'Naran Kaghan',
      description: 'Alpine meadows and crystal clear lakes',
      price: 179,
      rating: 4.5,
      image: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=400&h=300&fit=crop',
      location: 'Khyber Pakhtunkhwa',
      duration: '3-5 days',
      difficulty: 'Easy'
    },
    {
      id: 6,
      name: 'Fairy Meadows',
      description: 'Heaven on earth with Nanga Parbat views',
      price: 349,
      rating: 4.9,
      image: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=400&h=300&fit=crop',
      location: 'Gilgit-Baltistan',
      duration: '4-6 days',
      difficulty: 'Moderate'
    }
  ];

  // Featured hotels data
  featuredHotels: Hotel[] = [
    {
      id: 1,
      name: 'Serena Hotel Islamabad',
      location: 'Islamabad, Pakistan',
      price: 150,
      rating: 4.8,
      badge: 'eco-friendly',
      image: 'https://images.unsplash.com/photo-1566073771259-6a8506099945?w=400&h=300&fit=crop',
      description: 'Luxury hotel with world-class amenities and eco-friendly practices',
      amenities: [
        { name: 'WiFi', icon: 'fas fa-wifi' },
        { name: 'Pool', icon: 'fas fa-swimming-pool' },
        { name: 'Spa', icon: 'fas fa-spa' },
        { name: 'Restaurant', icon: 'fas fa-utensils' }
      ]
    },
    {
      id: 2,
      name: 'Pearl Continental Lahore',
      location: 'Lahore, Pakistan',
      price: 120,
      rating: 4.6,
      badge: 'luxury',
      image: 'https://images.unsplash.com/photo-1566073771259-6a8506099945?w=400&h=300&fit=crop',
      description: 'Historic luxury hotel in the heart of Lahore',
      amenities: [
        { name: 'WiFi', icon: 'fas fa-wifi' },
        { name: 'Gym', icon: 'fas fa-dumbbell' },
        { name: 'Restaurant', icon: 'fas fa-utensils' },
        { name: 'Bar', icon: 'fas fa-glass-martini' }
      ]
    },
    {
      id: 3,
      name: 'Hunza Embassy Hotel',
      location: 'Hunza Valley, Pakistan',
      price: 80,
      rating: 4.4,
      badge: 'eco-friendly',
      image: 'https://images.unsplash.com/photo-1566073771259-6a8506099945?w=400&h=300&fit=crop',
      description: 'Mountain view hotel with traditional hospitality',
      amenities: [
        { name: 'WiFi', icon: 'fas fa-wifi' },
        { name: 'Mountain View', icon: 'fas fa-mountain' },
        { name: 'Restaurant', icon: 'fas fa-utensils' },
        { name: 'Garden', icon: 'fas fa-seedling' }
      ]
    }
  ];

  // Car rental data
  carRentals: CarRental[] = [
    {
      id: 1,
      name: 'Toyota Corolla',
      type: 'Economy',
      price: 25,
      rating: 4.5,
      image: 'https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=400&h=300&fit=crop',
      features: ['Air Conditioning', 'Bluetooth', 'Fuel Efficient'],
      transmission: 'Automatic',
      seats: 5
    },
    {
      id: 2,
      name: 'Honda Civic',
      type: 'Compact',
      price: 30,
      rating: 4.6,
      image: 'https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=400&h=300&fit=crop',
      features: ['Air Conditioning', 'GPS Navigation', 'Backup Camera'],
      transmission: 'Automatic',
      seats: 5
    },
    {
      id: 3,
      name: 'Toyota Fortuner',
      type: 'SUV',
      price: 60,
      rating: 4.7,
      image: 'https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=400&h=300&fit=crop',
      features: ['4WD', 'Air Conditioning', 'Spacious Interior'],
      transmission: 'Automatic',
      seats: 7
    },
    {
      id: 4,
      name: 'Suzuki Mehran',
      type: 'Budget',
      price: 15,
      rating: 4.2,
      image: 'https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=400&h=300&fit=crop',
      features: ['Fuel Efficient', 'Easy Parking', 'Reliable'],
      transmission: 'Manual',
      seats: 4
    }
  ];

  // Testimonials data
  testimonials: Testimonial[] = [
    {
      id: 1,
      name: 'Sarah Ahmed',
      location: 'Karachi, Pakistan',
      text: 'Amazing experience! The team at Adventure Adorn made our trip to Hunza unforgettable. The accommodations were perfect and the local guides were incredibly knowledgeable.',
      avatar: 'https://images.unsplash.com/photo-1494790108755-2616b612b786?w=100&h=100&fit=crop&crop=face',
      rating: 5
    },
    {
      id: 2,
      name: 'Ahmed Khan',
      location: 'Lahore, Pakistan',
      text: 'Professional service and incredible destinations. The booking process was smooth and the trip exceeded our expectations. Highly recommended!',
      avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100&h=100&fit=crop&crop=face',
      rating: 5
    },
    {
      id: 3,
      name: 'Fatima Zahra',
      location: 'Islamabad, Pakistan',
      text: 'Our family trip to Swat Valley was perfect. The kids loved the adventure activities and we enjoyed the beautiful scenery. Will definitely book again!',
      avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=100&h=100&fit=crop&crop=face',
      rating: 4
    }
  ];

  constructor() { }

  // Get all destinations
  getDestinations(): Destination[] {
    return this.popularDestinations;
  }

  // Get destination by ID
  getDestinationById(id: number): Destination | undefined {
    return this.popularDestinations.find(dest => dest.id === id);
  }

  // Get all hotels
  getHotels(): Hotel[] {
    return this.featuredHotels;
  }

  // Get hotel by ID
  getHotelById(id: number): Hotel | undefined {
    return this.featuredHotels.find(hotel => hotel.id === id);
  }

  // Get all car rentals
  getCarRentals(): CarRental[] {
    return this.carRentals;
  }

  // Get car rental by ID
  getCarRentalById(id: number): CarRental | undefined {
    return this.carRentals.find(car => car.id === id);
  }

  // Get all testimonials
  getTestimonials(): Testimonial[] {
    return this.testimonials;
  }

  // Update search data
  updateSearchData(data: any) {
    this.searchData.next(data);
  }

  // Search destinations
  searchDestinations(query: string): Destination[] {
    return this.popularDestinations.filter(dest =>
      dest.name.toLowerCase().includes(query.toLowerCase()) ||
      dest.location.toLowerCase().includes(query.toLowerCase())
    );
  }

  // Search hotels
  searchHotels(query: string): Hotel[] {
    return this.featuredHotels.filter(hotel =>
      hotel.name.toLowerCase().includes(query.toLowerCase()) ||
      hotel.location.toLowerCase().includes(query.toLowerCase())
    );
  }

  // Filter destinations by price range
  filterDestinationsByPrice(min: number, max: number): Destination[] {
    return this.popularDestinations.filter(dest =>
      dest.price >= min && dest.price <= max
    );
  }

  // Filter hotels by price range
  filterHotelsByPrice(min: number, max: number): Hotel[] {
    return this.featuredHotels.filter(hotel =>
      hotel.price >= min && hotel.price <= max
    );
  }
}
