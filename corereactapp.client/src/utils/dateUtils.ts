export function formatDate(dateString: string | undefined): string {
    if (!dateString) return " - ";

    const date = new Date(dateString);
    return date.toLocaleString("hr-HR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
    });
}

export function normalizeDate(date: Date | null): Date | null {
    const normalizedDate = date
        ? new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()))
        : null;

    return normalizedDate;
}

export const isStartDateValid = (startDate: Date | null): boolean => {
    if (!startDate) return false;
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    return startDate >= today;
};
