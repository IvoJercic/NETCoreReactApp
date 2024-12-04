import axios from 'axios';
import { FlightDTO } from '../interfaces/FlightDTO';
import { IFlightFilter } from '../interfaces/IFlightFilter';

const SERVER_BASE_URL = "https://localhost:7278";
const BASE_URL = `${SERVER_BASE_URL}/api/Flight`;

export async function fetchFilteredFlights(filterData: IFlightFilter): Promise<FlightDTO[] | null> {
    try {
        const url = `${BASE_URL}/filter`;
        const response = await axios.post(url, filterData);
        return response.data;
    } catch {
        throw new Error('Failed to fetch filtered flights');
    }
}

export async function saveFlights(selectedItem: FlightDTO | null): Promise<boolean> {
    try {
        const url = `${BASE_URL}/CreateFlight`;
        const response = await axios.post(url, selectedItem);

        if (response.status !== 200) {
            throw new Error('Failed to save item');
        }
        return response.data;
    } catch {
        throw new Error('Failed to save item');
    }
}

export async function fetchFavorites(): Promise<FlightDTO[] | null> {
    try {
        const url = `${BASE_URL}/Favorites`;
        const response = await axios.get(url);

        if (response.status !== 200) {
            throw new Error('Failed to fetch items');
        }
        return response.data;
    } catch {
        throw new Error('Failed to fetch items');
    }
}

export async function deleteFlight(flightId: number): Promise<boolean> {
    try {
        const url = `${BASE_URL}/DeleteFlight/${flightId}`;
        const response = await axios.delete(url);

        if (response.status !== 200) {
            throw new Error('Failed to delete item');
        }

        return response.data;
    } catch {
        throw new Error('Failed to delete item');
    }
}
