interface ContractHolder {
    individualId: string;
    individualName: string;
    individualCPF: string;
    individualRG: string;
    individualEmail: string;
    individualBirthdate: string;
    isDeleted: boolean;
    individualTelephones: IndividualTelephonesItem[];
    individualAddresses: IndividualAddressesItem[];
}
interface IndividualTelephonesItem {
    telephoneId: string;
    telephoneNumber: string;
    telephoneType: number;
}
interface IndividualAddressesItem {
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
