import axios from "axios";
import { CompanyProfile, CompanySearch } from "./company";

interface SearchResponce {
  data: CompanySearch[] | string;
}

export const searchCompanies = async (queryString: string) => {
  try {
    const data = await axios.get<SearchResponce>(
      `https://financialmodelingprep.com/api/v3/search-name?query=${queryString}&limit=10&exchange=NASDAQ&apikey=kHQtMKuS1yWGeD0GmYCvS8uoTtRlEhaJ`
    );
    return data;
  } catch (e) {
    if (axios.isAxiosError(e)) {
      console.log("Error Message : ", e.message);
    } else {
      console.log("unexpected error: ", e);
      return "An unexpected error has occurred";
    }
  }
};

export const getCompanyProfile = async (query: string) => {
  try {
    const data = await axios.get<CompanyProfile[]>(
      `https://financialmodelingprep.com/api/v3/profile/${query}?apikey=kHQtMKuS1yWGeD0GmYCvS8uoTtRlEhaJ`
    );
    return data;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
};
