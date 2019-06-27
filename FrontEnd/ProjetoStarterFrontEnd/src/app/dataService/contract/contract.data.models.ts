interface Contract {
    signedContractId: string;
    contractHolderId: string;
    contractHolder: ContractHolder;
    type: number;
    category: number;
    expiryDate: string;
    isActive: boolean;
    beneficiariesIds: null;
    individuals: IndividualsItem[] | null;
    realties: null | RealtiesItem[];
    mobileDevices: null | MobileDevicesItem[];
    pets: null | PetsItem[];
    vehicles: null | VehiclesItem[];
}
interface ContractHolder {
    individualName: string;
    individualCPF: string;
    individualRG: string;
    individualEmail: string;
    individualBirthdate: string;
    beneficiaryId: string;
    isDeleted: boolean;
}
interface IndividualsItem {
    individualName: string;
    individualCPF: string;
    individualRG: string;
    individualEmail: string;
    individualBirthdate: string;
    beneficiaryId: string;
    isDeleted: boolean;
}
interface RealtiesItem {
    id: string;
    municipalRegistration: string | null;
    constructionDate: string;
    saleValue: number;
    marketValue: number;
    address: null | Address;
    addressId: string;
    addressStreet: null;
    addressNumber: null;
    addressComplement: null;
    addressNeighborhood: null;
    addressCity: null;
    addressState: null;
    addressCountry: null;
    addressZipCode: null;
    addressType: number;
    beneficiaryId: string;
    isDeleted: boolean;
}
interface Address {
    addressId: string;
    addressStreet: string;
    addressNumber: string;
    addressComplement: string;
    addressNeighborhood: string;
    addressCity: string;
    addressState: string;
    addressCountry: string;
    addressZipCode: string;
    addressType: number;
}
interface MobileDevicesItem {
    mobileDeviceBrand: string;
    mobileDeviceModel: string;
    mobileDeviceSerialNumber: string;
    mobileDeviceManufactoringYear: string;
    mobileDeviceType: number;
    mobileDeviceInvoiceValue: number;
    beneficiaryId: string;
    isDeleted: boolean;
}
interface PetsItem {
    petName: string;
    petSpecies: number;
    petBreed: string;
    petBirthdate: string;
    beneficiaryId: string;
    isDeleted: boolean;
}
interface VehiclesItem {
    vehicleBrand: string;
    vehicleModel: string;
    vehicleManufactoringYear: string;
    vehicleColor: number;
    vehicleModelYear: string;
    vehicleChassisNumber: string;
    vehicleCurrentMileage: number;
    vehicleCurrentFipeValue: number;
    vehicleDoneInspection: boolean;
    beneficiaryId: string;
    isDeleted: boolean;
}
