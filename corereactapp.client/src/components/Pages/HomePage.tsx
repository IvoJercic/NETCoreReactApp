import FlightFilterForm from '../FlightFilterForm';
import FlightTable from '../FlightTable';

export default function HomePage() {
    return (
        <div>
            <FlightFilterForm />
            <FlightTable data={null} />
        </div>
    );
}
