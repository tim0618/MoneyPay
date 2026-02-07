<template>
  <div class="q-pa-md">
    
    <div class="cyber-card">
      <div class="corner top-left"></div>
      <div class="corner top-right"></div>
      <div class="corner bottom-left"></div>
      <div class="corner bottom-right"></div>

      <div class="card-header">
        <span class="header-deco">>></span> 
        ANALYSIS_01 :: MONTHLY_BALANCE
        <div class="scan-line"></div>
      </div>
      
      <div class="chart-container monitor-screen">
        <apexchart
          type="bar"
          height="300"
          :options="balanceChartOptions"
          :series="balanceChartSeries"
        >
        </apexchart>
      </div>
    </div>

    <div class="cyber-card">
      <div class="corner top-left"></div>
      <div class="corner top-right"></div>
      <div class="corner bottom-left"></div>
      <div class="corner bottom-right"></div>

      <div class="card-header">
        <span class="header-deco">>></span> 
        ANALYSIS_02 :: EXPENSE_RATIO
        <div class="scan-line"></div>
      </div>

      <div v-if="moneyStore.totalExpense === 0" class="no-data">
        <q-icon name="warning" size="md" color="grey-7" />
        <div class="q-mt-sm">// NULL_DATA: SYSTEM_EMPTY</div>
      </div>

      <div v-else class="chart-container monitor-screen">
        <apexchart
          type="donut"
          height="350"
          :options="expenseChartOptions"
          :series="expenseChartSeries"
        >
        </apexchart>
      </div>
    </div>

  </div>
</template>

<style scoped>
/* 定義局部變數 */
.cyber-card {
  --neon-color: #00ffcc;
  --bg-color: rgba(10, 15, 20, 0.85);
  --border-color: #333;
  
  position: relative;
  background: var(--bg-color);
  border: 1px solid var(--border-color);
  margin-bottom: 30px;
  padding: 20px;
  
  /* 直角風格 */
  border-radius: 0; 
  
  /* 科技感陰影 */
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.5);
  
  /* 背景網格裝飾 */
  background-image: 
    linear-gradient(rgba(0, 255, 204, 0.03) 1px, transparent 1px),
    linear-gradient(90deg, rgba(0, 255, 204, 0.03) 1px, transparent 1px);
  background-size: 20px 20px;
}

/* 標題區域 */
.card-header {
  font-family: 'Share Tech Mono', monospace;
  font-size: 1.2rem;
  color: var(--neon-color);
  margin-bottom: 20px;
  text-transform: uppercase;
  letter-spacing: 2px;
  position: relative;
  padding-bottom: 10px;
  text-shadow: 0 0 5px var(--neon-color);
}

.header-deco {
  margin-right: 10px;
  animation: blink 1s infinite;
}

/* 標題下方的掃描線動畫 */
.scan-line {
  position: absolute;
  bottom: 0;
  left: 0;
  height: 2px;
  background: var(--neon-color);
  width: 100%;
  opacity: 0.5;
  box-shadow: 0 0 10px var(--neon-color);
}

/* 圖表容器 - 模擬螢幕 */
.chart-container {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  /* 給圖表一個稍微深一點的背景框，突顯內容 */
  background: rgba(0, 0, 0, 0.3); 
  border: 1px solid #222;
  padding: 10px;
}

/* 無數據狀態 */
.no-data {
  text-align: center;
  color: #666;
  padding: 60px 0;
  font-family: 'Share Tech Mono', monospace;
  border: 1px dashed #444;
  background: rgba(0,0,0,0.2);
}

/* --- 裝飾性角落 (Sci-Fi Corners) --- */
.corner {
  position: absolute;
  width: 10px;
  height: 10px;
  border-color: var(--neon-color);
  border-style: solid;
  transition: all 0.3s;
}

.top-left {
  top: -1px; left: -1px;
  border-width: 2px 0 0 2px;
}

.top-right {
  top: -1px; right: -1px;
  border-width: 2px 2px 0 0;
}

.bottom-left {
  bottom: -1px; left: -1px;
  border-width: 0 0 2px 2px;
}

.bottom-right {
  bottom: -1px; right: -1px;
  border-width: 0 2px 2px 0;
}

/* Hover 時角落發光 */
.cyber-card:hover .corner {
  width: 15px;
  height: 15px;
  box-shadow: 0 0 10px var(--neon-color);
}

@keyframes blink {
  0%, 100% { opacity: 1; }
  50% { opacity: 0.3; }
}
</style>

<script setup>
import { computed, onMounted } from "vue";
import { useMoneyStore } from "../stores/moneyStore";

const moneyStore = useMoneyStore();

onMounted(() => {
  moneyStore.initData();
});

// ==========================================
// 1. 收支對比圖 (Bar Chart) - 賽博龐克版
// ==========================================
const balanceChartSeries = computed(() => {
  return [
    {
      name: "金額",
      data: [moneyStore.totalIncome, moneyStore.totalExpense],
    },
  ];
});

const balanceChartOptions = computed(() => {
  return {
    chart: {
      id: "balance-chart",
      toolbar: { show: false },
      // ★ 關鍵：背景透明，字體改為數位字體
      background: "transparent",
      fontFamily: "'Share Tech Mono', monospace, sans-serif",
    },
    // ★ 關鍵：啟用暗黑模式，Tooltip 會自動變黑
    theme: {
      mode: 'dark', 
    },
    // ★ 關鍵：改用高飽和度的霓虹色
    // Income: 霓虹青 (#00ffcc) / Expense: 警示紅 (#ff0055)
    colors: ["#00ffcc", "#ff0055"],

    fill: {
      opacity: 1, // 科技感通常是實心或半透明，不要太柔和
      type: 'gradient', // 加上漸層更有質感
      gradient: {
        shade: 'dark',
        type: "horizontal",
        shadeIntensity: 0.5,
        inverseColors: true,
        opacityFrom: 1,
        opacityTo: 0.6,
        stops: [0, 100]
      }
    },
    
    plotOptions: {
      bar: {
        borderRadius: 0, // ★ 關鍵：去除圓角，改為直角硬派風格
        horizontal: true,
        distributed: true,
        barHeight: "50%", 
      },
    },
    
    dataLabels: {
      enabled: true,
      textAnchor: 'start',
      style: {
        fontSize: "14px",
        fontFamily: "'Share Tech Mono', monospace",
        colors: ["#fff"], 
      },
      formatter: function (val, opt) {
        return "$ " + val
      },
      offsetX: 0,
    },
    
    xaxis: {
      categories: ["總收入", "總支出"],
      labels: {
        style: { 
          colors: "#00ffcc", // X軸文字改為霓虹色
          fontSize: '12px',
          fontFamily: "'Share Tech Mono', monospace",
        }, 
      },
      axisBorder: { show: false },
      axisTicks: { show: false },
    },
    
    yaxis: {
      labels: {
        style: {
          colors: "#fff", // Y軸文字白色
          fontSize: '14px',
          fontFamily: "'Share Tech Mono', monospace",
        }
      }
    },

    grid: {
      borderColor: "#333", // 格線改深色
      strokeDashArray: 4,  // 格線改虛線，像儀表板
      xaxis: {
        lines: { show: true }
      },   
      yaxis: {
        lines: { show: false }
      },
    },
    
    tooltip: {
      theme: 'dark',
      style: {
        fontSize: '12px',
        fontFamily: "'Share Tech Mono', monospace",
      },
      x: { show: false } // 不顯示 Title，直接顯示數值比較俐落
    },
    
    legend: { show: false },
  };
});

// ==========================================
// 2. 支出圓餅圖 (Donut Chart) - 賽博龐克版
// ==========================================

const activeExpenses = computed(() => {
  return moneyStore.expenseList.filter((item) => item.totalPay > 0);
});

const expenseChartSeries = computed(() => {
  return activeExpenses.value.map((item) => item.totalPay);
});

const expenseChartOptions = computed(() => {
  const labels = activeExpenses.value.map((item) => item.typeName);
  // 如果你的 moneyStore 裡的顏色是柔和色系，建議這裡可以考慮強制覆蓋成一組霓虹色盤
  // 但為了保持一致性，先沿用 store 顏色，靠邊框修飾
  const colors = activeExpenses.value.map((item) => item.color);

  return {
    chart: {
      type: "donut",
      background: "transparent",
      fontFamily: "'Share Tech Mono', monospace, sans-serif",
    },
    theme: {
      mode: 'dark',
    },
    labels: labels,
    colors: colors,

    // ★ 關鍵：不要讓顏色太透，實色在黑底上才好看
    fill: {
      opacity: 1,
    },

    // ★ 關鍵：邊框改成深黑色 (配合你的背景色 #0a0f14)
    // 原本是白色邊框，在黑底會變成一圈圈白線，很醜
    stroke: {
      show: true,
      colors: ["#0a0f14"], 
      width: 2,
    },

    plotOptions: {
      pie: {
        donut: {
          size: "70%", 
          labels: {
            show: true,
            name: {
              show: true,
              fontSize: "12px",
              fontFamily: "'Share Tech Mono', monospace",
              color: "#00ffcc", // 分類名稱改霓虹色
              offsetY: -5,
            },
            value: {
              show: true,
              fontSize: "20px",
              fontFamily: "'Share Tech Mono', monospace",
              color: "#ffffff", // 金額改亮白
              offsetY: 5,
              formatter: (val) => `$${val}`,
            },
            total: {
              show: true,
              showAlways: true,
              label: "TOTAL", // 改英文比較帥
              fontSize: "12px",
              fontFamily: "'Share Tech Mono', monospace",
              color: "#00ffcc",
              formatter: () => `$${moneyStore.totalExpense}`,
            },
          },
        },
      },
    },
    
    dataLabels: { 
      enabled: false // 關閉甜甜圈上的數字，保持簡潔
    },
    
    legend: {
      position: "bottom",
      fontFamily: "'Share Tech Mono', monospace",
      labels: {
        colors: "#ccc", // 圖例文字改淺灰
      },
      markers: {
        width: 10,
        height: 10,
        radius: 0, // ★ 圖例標記改方形 (直角)
      },
      itemMargin: {
        horizontal: 10,
        vertical: 5
      }
    },
    
    tooltip: {
      theme: 'dark',
      style: {
         fontFamily: "'Share Tech Mono', monospace",
      },
      y: {
        formatter: function(val) {
          return "$ " + val
        }
      }
    }
  };
});
</script>

