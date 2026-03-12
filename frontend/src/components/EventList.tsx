import React, { useEffect, useState } from "react";
import { getEvents } from "../api";

export default function EventList() {
  const [events, setEvents] = useState<any[]>([]);

  useEffect(() => {
    getEvents().then(setEvents).catch(console.error);
  }, []);

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold">Eventos registrados</h2>
      <ul>
        {events.map(ev => (
          <li key={ev.id} className="border p-2 mb-2">
            <strong>{ev.name}</strong> - {ev.date} - {ev.location}
          </li>
        ))}
      </ul>
    </div>
  );
}
