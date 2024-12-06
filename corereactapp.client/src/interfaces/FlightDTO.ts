export interface FlightDTO {
    id: number;
    startSourceIATA: string;
    startSourceLocation: string;
    startDestinationIATA: string;
    startDestinationLocation: string;
    startDate: string;
    startArrivalDate: string;
    startNumberOfStops: number;

    endSourceIATA?: string; 
    endDestinationIATA?: string; 
    endDate?: string;  
    endArrivalDate?: string;  
    endNumberOfStops?: number; 

    bookableSeats: number;
    currency: string;
    price: number;

    startFlightDuration: string;
    endFlightDuration?: string; 
}
