import React, { createContext, useContext, useState, ReactNode } from 'react';
import { FlightDTO } from '../interfaces/FlightDTO';

interface FlightContextType {
    loading: boolean;
    setLoading: React.Dispatch<React.SetStateAction<boolean>>;
    response: FlightDTO[] | null;
    setResponse: React.Dispatch<React.SetStateAction<FlightDTO[] | null>>;
    favorites: FlightDTO[] | null;
    setFavorites: React.Dispatch<React.SetStateAction<FlightDTO[] | null>>;
    refreshFavoritesTrigger: boolean;
    setRefreshFavoritesTrigger: React.Dispatch<React.SetStateAction<boolean>>;
}

const FlightContext = createContext<FlightContextType | undefined>(undefined);

export const FlightProvider = ({ children }: { children: ReactNode }) => {
    const [loading, setLoading] = useState(false);
    const [response, setResponse] = useState<FlightDTO[] | null>(null);
    const [favorites, setFavorites] = useState<FlightDTO[] | null>(null);
    const [refreshFavoritesTrigger, setRefreshFavoritesTrigger] = useState<boolean>(false);

    const value = {
        loading,
        setLoading,
        response,
        setResponse,
        favorites,
        setFavorites,
        refreshFavoritesTrigger,
        setRefreshFavoritesTrigger,
    };

    return (
        <FlightContext.Provider value={ value }>
            {children}
        </FlightContext.Provider>
    );
};

export const useFlightContext = (): FlightContextType => {
    const context = useContext(FlightContext);
    if (!context) {
        throw new Error('useFlightContext must be used within a FlightProvider');
    }
    return context;
};
