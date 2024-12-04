export default function LoaderComponent() {
    return (
        <div className="loader-container">
            <div className="spinner-border text-primary" role="status">
                <span className="visually-hidden">Loading...</span>
            </div>
        </div>
    );
}