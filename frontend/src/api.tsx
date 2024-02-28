import axios from "axios";
import {
  CompanyBalanceSheet,
  CompanyCashFlow,
  CompanyIncomeStatement,
  CompanyKeyMetrics,
  CompanyProfile,
  CompanySearch,
} from "./company";

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

export const getCompanyMetrics = async (query: string) => {
  try {
    const data = await axios.get<CompanyKeyMetrics[]>(
      `https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?apikey=kHQtMKuS1yWGeD0GmYCvS8uoTtRlEhaJ`
    );
    return data;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
};

export const getIncomeMetrics = async (query: string) => {
  try {
    const data = await axios.get<CompanyIncomeStatement[]>(
      `https://financialmodelingprep.com/api/v3/income-statement/${query}?period=annual&apikey=kHQtMKuS1yWGeD0GmYCvS8uoTtRlEhaJ`
    );
    return data;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
};

export const getIncomeBalanceSheet = async (query: string) => {
  try {
    const data = await axios.get<CompanyBalanceSheet[]>(
      `https://financialmodelingprep.com/api/v3/balance-sheet-statement/${query}?period=annual&apikey=kHQtMKuS1yWGeD0GmYCvS8uoTtRlEhaJ`
    );
    return data;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
};

export const getCashFlowBalance = async (query: string) => {
  try {
    const data = await axios.get<CompanyCashFlow[]>(
      `https://financialmodelingprep.com/api/v3/cash-flow-statement/${query}?period=annual&apikey=kHQtMKuS1yWGeD0GmYCvS8uoTtRlEhaJ`
    );
    return data;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
};
