import { CSmartTable, CButton, CCardBody, CCollapse } from "@coreui/react-pro";
import { useState } from "react";
import { FlightDTO } from "../interfaces/FlightDTO";
import { formatDate } from "../utils/dateUtils";
import { useFlightContext } from "../context/FlightContext";
import { deleteFlight, saveFlights } from "../api/FlightApi";

interface FlightTableProps {
    data: FlightDTO[] | null; 
}

export default function FlightTable({ data }: FlightTableProps) {
    const { response, setRefreshFavoritesTrigger, setResponse, setFavorites } = useFlightContext(); 
    const itemsToDisplay = data ? data : response;

    const columns = [
        {
            key: "Id",
            _style: { width: "5%" },
            filter: false,
            sorter: true,
        },
        {
            key: "StartSourceIATA",
            label: "From",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "StartDestinationIATA",
            label: "To",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "StartDate",
            label: "At",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "StartArrivalDate",
            label: "Arrive at",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "StartNumberOfStops",
            label: "Stops",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "StartFlightDuration",
            label: "Duration",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "EndDate",
            label: "Come back on",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "EndArrivalDate",
            label: "Arrive back on",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "EndNumberOfStops",
            label: "Stops",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "EndFlightDuration",
            label: "Duration",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "BookableSeats",
            label: "Free seats",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "Currency",
            label: "Currency",
            _style: { width: "10%" },
            filter: true,
            sorter: true,
        },
        {
            key: "Price",
            label: "Price",
            _style: { width: "10%" },
            filter: false,
            sorter: false,
        },                
        {
            key: "show_details",
            label: "",
            _style: { width: "1%" },
            filter: false,
            sorter: false,
        },
        {
            key: "add_to_favorites",
            label: "",
            _style: { width: "1%" },
            filter: false,
            sorter: false,
        },
        {
            key: "delete",
            label: "",
            _style: { width: "1%" },
            filter: false,
            sorter: false,
        },
    ];
    const [details, setDetails] = useState<Array<number>>([]);

    const toggleDetails = (id: number) => {
        const position = details.indexOf(id);
        const newDetails = [...details];
        if (position !== -1) {
            newDetails.splice(position, 1);
        } else {
            newDetails.push(id);
        }
        setDetails(newDetails);
    };

    const handleDelete = async (flightId: number) => {
        const data = await deleteFlight(flightId);
        if (data) {
            setRefreshFavoritesTrigger(true);
            setFavorites((prevResponse) => {
                const updatedResponse = prevResponse?.filter((item) => item.id !== flightId);
                return updatedResponse || null;
            });
            alert("Deleted successfully");
        }
        else {
            alert("Error occurred");
        }

    };

    const handleAddToFavorites = async (flight: FlightDTO) => {
        const data = await saveFlights(flight);
        if (data) {
            setRefreshFavoritesTrigger(true);
            setResponse((prevResponse) => {
                const updatedResponse = prevResponse?.filter((item) => item.id !== flight.id);
                return updatedResponse || null; 
            });
            alert("Added successfully");
        }
        else {
            alert("Error occurred");
        }
    };

    return (
        <div>
            {itemsToDisplay && itemsToDisplay?.length > 0 && (
                <>
                    <CSmartTable
                        activePage={1}
                        columns={columns}
                        items={itemsToDisplay ?? []}
                        itemsPerPageSelect
                        itemsPerPage={5}
                        pagination
                        scopedColumns={{
                            Id: (item: FlightDTO) => <td>{item.id}</td>,
                            StartSourceIATA: (item: FlightDTO) => (
                                <td>{item.startSourceIATA}</td>
                            ),
                            StartDestinationIATA: (item: FlightDTO) => (
                                <td>{item.startDestinationIATA}</td>
                            ),
                            StartDate: (item: FlightDTO) => (
                                <td>{formatDate(item.startDate)}</td>
                            ),
                            StartArrivalDate: (item: FlightDTO) => (
                                <td>{formatDate(item.startArrivalDate)}</td>
                            ),
                            StartNumberOfStops: (item: FlightDTO) => (
                                <td>{item.startNumberOfStops}</td>
                            ),                            
                            EndDate: (item: FlightDTO) => <td>{formatDate(item.endDate)}</td>,
                            EndArrivalDate: (item: FlightDTO) => (
                                <td>{formatDate(item.endArrivalDate)}</td>
                            ),
                            EndNumberOfStops: (item: FlightDTO) => (
                                <td>{item.endNumberOfStops ?? 0}</td>
                            ),
                            BookableSeats: (item: FlightDTO) => <td>{item.bookableSeats}</td>,
                            Currency: (item: FlightDTO) => <td>{item.currency}</td>,
                            Price: (item: FlightDTO) => <td>{item.price}</td>,
                            StartFlightDuration: (item: FlightDTO) => (
                                <td>{item.startFlightDuration}</td>
                            ),
                            EndFlightDuration: (item: FlightDTO) => (
                                <td>{item.endFlightDuration || "-"}</td>
                            ),
                            show_details: (item: FlightDTO) => (
                                <td className="py-2">
                                    <CButton
                                        color="primary"
                                        variant="outline"
                                        shape="square"
                                        size="sm"
                                        onClick={() => toggleDetails(item.id)}
                                    >
                                        {details.includes(item.id) ? "Hide" : "Show"}
                                    </CButton>
                                </td>
                            ),
                            details: (item: FlightDTO) => (
                                <CCollapse visible={details.includes(item.id)}>
                                    <CCardBody className="p-3">
                                        <h4>Flight Details</h4>

                                        <div className="flight-details-container">
                                            <p className="flight-details">
                                                <strong>Start:</strong> {item.startSourceIATA} {item.startSourceLocation ?? ""} to {item.startDestinationIATA} {item.startDestinationLocation ?? ""}<br />
                                                <strong>Departure Date:</strong> {formatDate(item.startDate)} <br />
                                                <strong>Arrival Date:</strong> {formatDate(item.startArrivalDate)} <br />
                                                <strong>Stops:</strong> {item.startNumberOfStops} {item.startNumberOfStops === 1 ? 'stop' : 'stops'} <br />
                                                <strong>Flight Duration:</strong> {item.startFlightDuration} <br />
                                            </p>

                                            <p className="flight-details" style={{ alignSelf:"center" }}>
                                                <strong>Available Seats:</strong> {item.bookableSeats} <br />
                                                <strong>Price:</strong> {item.currency} {item.price} <br />
                                            </p>

                                            {item.endDate && (
                                                <p className="flight-details">
                                                    <strong>End:</strong> {item.endSourceIATA} {item.startDestinationLocation ?? ""} to {item.endDestinationIATA} {item.startSourceLocation ?? ""}<br />
                                                    <strong>Return Date:</strong> {formatDate(item.endDate)} <br />
                                                    <strong>Return Arrival Date:</strong> {formatDate(item.endArrivalDate)} <br />
                                                    <strong>Return Stops:</strong> {item.endNumberOfStops ?? 0} {item.endNumberOfStops === 1 ? 'stop' : 'stops'} <br />
                                                    <strong>Return Flight Duration:</strong> {item.endFlightDuration ?? "N/A"} <br />
                                                </p>
                                            )}
                                        </div>
                                    </CCardBody>
                                </CCollapse>
                            ),
                            add_to_favorites: (item: FlightDTO) => (
                                <td>
                                    {!data || !data.find((flight) => flight.id === item.id) ? (
                                        <CButton
                                            color="success"
                                            size="sm"
                                            onClick={() => handleAddToFavorites(item)}
                                        >
                                            Add to favorites
                                        </CButton>
                                    ) : (
                                        <span></span>
                                    )}
                                </td>
                            ),
                            ...(data
                                ? {
                                    delete: (item: FlightDTO) => (
                                        <td>
                                            <CButton
                                                color="danger"
                                                size="sm"
                                                onClick={() =>
                                                    handleDelete(item.id)
                                                }
                                            >
                                                Delete
                                            </CButton>
                                        </td>
                                    ),
                                }
                                : {}),
                        }}
                        sorterValue={{ column: "SourceIATA", state: "asc" }}
                        tableProps={{
                            className: "add-this-class",
                            responsive: true,
                            striped: true,
                            hover: true,
                        }}
                        tableBodyProps={{
                            className: "align-middle",
                        }}
                    />
                </>
            )}
        </div>
    );
}
