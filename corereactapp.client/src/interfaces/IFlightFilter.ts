export interface IFlightFilter {
    sourceIATA: string;
    destinationIATA: string;
    startDate: Date | null;
    endDate: Date | null;
    passengers: number | null;
    currency: string | null;
}
