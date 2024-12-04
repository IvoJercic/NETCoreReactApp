import { CButton, CForm, CFormInput, CFormLabel, CFormText } from "@coreui/react-pro";
import React, { useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { ErrorMessages } from "../constants/ErrorMessages";
import { FieldIds } from "../constants/FieldIds";
import { IFlightFilter } from "../interfaces/IFlightFilter";
import { fetchFilteredFlights } from "../api/FlightApi";
import { useFlightContext } from "../context/FlightContext";
import { isStartDateValid, normalizeDate } from "../utils/dateUtils";

export default function FlightFilterForm() {
    const { loading, setLoading, setResponse } = useFlightContext();

    const [filterData, setFilterData] = useState<IFlightFilter>({
        sourceIATA: "",
        destinationIATA: "",
        startDate: null,
        endDate: null,
        passengers: null,
        currency: "",
    });

    const [errors, setErrors] = useState<{ [key in FieldIds]?: string }>({
        sourceIATA: "",
        destinationIATA: "",
        endDate: "",
        startDate: "",
    });

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const filteredfilterData = Object.fromEntries(
            Object.entries(filterData).filter(([, value]) => value !== null && value !== "" && value !== false),
        );

        try {
            setLoading(true);
            const response = await fetchFilteredFlights(filteredfilterData as IFlightFilter);
            setResponse(response);
        } catch (error) {
            console.error("Error submitting form data:", error);
        } finally {
            setLoading(false);
        }
    };

    const validateIATA = (field: keyof typeof ErrorMessages, value: string) => {
        if (!(value.length === 3 && value.length > 0)) {
            setErrors((prevErrors) => ({
                ...prevErrors,
                [field]: ErrorMessages[field],
            }));
        } else {
            setErrors((prevErrors) => ({
                ...prevErrors,
                [field]: "",
            }));
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        const field = id as string;
        setFilterData((prevData: IFlightFilter) => ({
            ...prevData,
            [field]: value,
        }));

        if (field === FieldIds.sourceIATA || field === FieldIds.destinationIATA) {
            validateIATA(field, value);
        }
    };

    const validateEndDate = (startDate: Date | null, endDate: Date | null) => {
        if (startDate && endDate && endDate < startDate) {
            setErrors((prevErrors) => ({
                ...prevErrors,
                endDate: ErrorMessages.endDate,
            }));
        } else {
            setErrors((prevErrors) => ({
                ...prevErrors,
                endDate: "",
            }));
        }
    };

    const handleDateChange = (id: FieldIds, date: Date | null) => {
        const normalizedDate = normalizeDate(date);

        if (id == FieldIds.startDate) {
            if (!isStartDateValid(normalizedDate)) {
                setErrors((prevErrors) => ({
                    ...prevErrors,
                    startDate: ErrorMessages.pastDate,
                }));
            } else {
                setErrors((prevErrors) => ({
                    ...prevErrors,
                    startDate: "",
                }));
            }
        }

        setFilterData((prevData: IFlightFilter) => ({
            ...prevData,
            [id]: normalizedDate,
        }));

        if (id == FieldIds.endDate) {
            validateEndDate(filterData.startDate, normalizedDate);
        } else {
            validateEndDate(normalizedDate, filterData.endDate);
        }
    };

    return (
        <CForm className="form-container" onSubmit={handleSubmit}>
            <div className="form-grid">
                <div className="left-side">
                    <div className="mb-4">
                        <CFormLabel htmlFor={FieldIds.sourceIATA}>Fly from</CFormLabel>*
                        <CFormInput
                            id={FieldIds.sourceIATA}
                            type="text"
                            aria-describedby={`${FieldIds.sourceIATA}Helper`}
                            value={filterData.sourceIATA}
                            placeholder="SPU"
                            onChange={handleInputChange}
                            maxLength={3}
                            className={`form-input ${errors.sourceIATA ? "input-error" : ""}`}
                            required={true}
                        />
                        <CFormText id={`${FieldIds.sourceIATA}Helper`}>
                            IATA Code of airport 
                        </CFormText>
                        {errors.sourceIATA && <div className="error-message">{errors.sourceIATA ?? ""}</div>}
                    </div>

                    <div className="mb-4">
                        <CFormLabel htmlFor={FieldIds.startDate}>Go on</CFormLabel>*
                        <br />
                        <DatePicker
                            id={FieldIds.startDate}
                            selected={filterData.startDate}
                            locale="en"
                            onChange={(date: Date | null) => handleDateChange(FieldIds.startDate, date)}
                            dateFormat="dd/MM/yyyy"
                            className="form-control date-picker"
                            required={true}
                        />
                        {errors.startDate && <div className="error-message">{errors.startDate ?? ""}</div>}
                    </div>
                </div>

                <div className="right-side">
                    <div className="mb-4">
                        <CFormLabel htmlFor={FieldIds.destinationIATA}>to</CFormLabel>*
                        <CFormInput
                            id={FieldIds.destinationIATA}
                            type="text"
                            aria-describedby={`${FieldIds.destinationIATA}Help`}
                            value={filterData.destinationIATA}
                            placeholder="ZAG"
                            onChange={handleInputChange}
                            maxLength={3}
                            className={`form-input ${errors.destinationIATA ? "input-error" : ""}`}
                            required={true}
                        />
                        <CFormText id={`${FieldIds.destinationIATA}Help`}>
                            IATA Code of airport 
                        </CFormText>
                        {errors.destinationIATA && <div className="error-message">{errors.destinationIATA ?? ""}</div>}
                    </div>

                    <div className="mb-4">
                        <CFormLabel htmlFor={FieldIds.endDate}>Come back on</CFormLabel>
                        <br />
                        <DatePicker
                            id={FieldIds.endDate}
                            selected={filterData.endDate}
                            locale="en"
                            onChange={(date: Date | null) => handleDateChange(FieldIds.endDate, date)}
                            dateFormat="dd/MM/yyyy"
                            className="form-control date-picker"
                        />
                        {errors.endDate && <div className="error-message">{errors.endDate}</div>}
                    </div>
                </div>
            </div>

            <div className="form-bottom">
                <div className="mb-4">
                    <CFormLabel htmlFor={FieldIds.passengers}>Passengers</CFormLabel>
                    <CFormInput
                        id={FieldIds.passengers}
                        type="number"
                        value={filterData.passengers ?? ""}
                        onChange={handleInputChange}
                        className="form-input"
                        min={0}
                    />
                </div>

                <div className="mb-4">
                    <CFormLabel htmlFor={FieldIds.currency}>Currency</CFormLabel>
                    <CFormInput
                        id={FieldIds.currency}
                        type="text"
                        aria-describedby={`${FieldIds.currency}Help`}
                        placeholder="EUR"
                        value={filterData.currency ?? ""}
                        onChange={handleInputChange}
                        className="form-input"
                    />
                </div>
            </div>

            <CButton type="submit" color="primary" className="submit-button" disabled={loading}>
                {loading ? "Submitting..." : "Submit"}
            </CButton>
        </CForm>
    );
}
