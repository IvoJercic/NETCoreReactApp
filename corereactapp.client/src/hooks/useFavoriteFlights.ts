import { useEffect } from "react";
import { fetchFavorites } from "../api/FlightApi";
import { useFlightContext } from "../context/FlightContext";

export function useFavoriteFlights() {
    const { loading, setLoading, favorites, setFavorites, refreshFavoritesTrigger, setRefreshFavoritesTrigger } = useFlightContext();

    useEffect(() => {
        const getData = async () => {
            if (favorites && !refreshFavoritesTrigger) return; // Skip if favorites exist and no refresh is needed
            setLoading(true);
            setRefreshFavoritesTrigger(false); 

            try {
                const data = await fetchFavorites();
                setFavorites(data);
            } catch (error) {
                console.error("Error fetching favorite flights:", error);
            } finally {
                setLoading(false);
                setRefreshFavoritesTrigger(false); 
            }
        };

        getData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [refreshFavoritesTrigger, setLoading, setFavorites, setRefreshFavoritesTrigger]);

    return { favorites, loading };
}
