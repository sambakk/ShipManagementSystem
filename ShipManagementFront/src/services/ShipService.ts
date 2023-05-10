import http from "../config/http-common";
import { Ship } from "../types/Ship";

class ShipService {
    getAll() {
        return http.get<Array<Ship>>("/ships");
    }

    get(id: string) {
        return http.get<Ship>(`/ships/${id}`);
    }

    create(data: Ship) {
        return http.post<Ship>("/ships", data);
    }

    update(data: Ship, id: any) {
        return http.put<any>(`/ships/${id}`, data);
    }

    delete(id: any) {
        return http.delete<any>(`/ships/${id}`);
    }

}

export default new ShipService();