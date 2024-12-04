import FlightTable from "../FlightTable";
import { useFavoriteFlights } from "../../hooks/useFavoriteFlights";

export default function FavoritesPage() {
    const { favorites, loading } = useFavoriteFlights();

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h2>Favorites</h2>
            <FlightTable data={favorites} />
        </div>
    );
}
