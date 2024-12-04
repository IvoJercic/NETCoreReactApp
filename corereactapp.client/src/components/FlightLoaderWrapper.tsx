import { useFlightContext } from "../context/FlightContext";
import LoaderComponent from "./LoaderComponent";
export default function FlightLoaderWrapper({ children }: { children: React.ReactNode }) {
    const { loading } = useFlightContext();

    return (
        <>
            {loading && <LoaderComponent />}
            {!loading && children}
        </>
    );
}
