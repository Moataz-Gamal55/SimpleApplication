import { useState, useEffect } from 'react'
import './App.css'

type Data = {
  message: string;
  servedBy: string;
  timestamp: string;
}

function App() {
  const [data, setData] = useState<Data | null>(null);
  const [error, setError] = useState(null);

  const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5220";

  useEffect(() => {
    fetch(`${API_URL}/api/message`)
      .then(response => {
        if (!response.ok) throw new Error("Network was not ok.");
        return response.json();
      })
      .then(responseData => setData(responseData))
      .catch(err => setError(err.message));
  }, [API_URL])

  return (
    <div className="App" style={{ padding: "50px", fontFamily: "Arial" }}>
      <div style={{ border: "1px solid #ccc", padding: "20px", borderRadius: "8px" }}>
        <h3>Backend Connection Status:</h3>

        {error && <p style={{ color: "red" }}>Error: {error}</p>}

        {data ? (
          <div style={{ color: "green" }}>
            <p><strong>Message:</strong> {data.message}</p>
            <p><strong>Server Pod ID:</strong> {data.servedBy}</p>
            <p><strong>Timestamp:</strong> {data.timestamp}</p>
          </div>
        ) : (
          !error && <p>Loading data from {API_URL}...</p>
        )}
      </div>

      <p style={{ marginTop: "20px", fontSize: "0.8em", color: "#666" }}>
        Targeting Backend at: <code>{API_URL}</code>
      </p>
    </div>
  );
}

export default App
