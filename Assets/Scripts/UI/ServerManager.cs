using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UI.Tables;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
   public TableLayout tableLayout;
   public JoiningController joinController;
   public List<ServerData> servers = new List<ServerData>();
   public GameObject privateIconPrefab;
   public GameObject textPrefab;
   public GameObject joinButtonPrefab;

   private void Awake()
   {
      foreach (var server in servers)
      {
         TableRow row = tableLayout.AddRow();
         row.preferredHeight = 120;
         InstantiateLockIcon(server.isPrivate, row, 0);
         InstantiateText(row, 1, server.title);
         InstantiateText(row, 2, server.ip);
         InstantiateText(row, 3, server.region);
         InstantiateText(row, 4, server.playersOnline);
         InstantiateJoin(server, row, 5);
      }
   }

   private void InstantiateLockIcon(bool isPrivate, TableRow row, int cellIndex)
   {
      if (!isPrivate) return;
      TableCell cell0 = row.Cells[0];
      Instantiate(privateIconPrefab,cell0.transform);
   }

   private void InstantiateText(TableRow row, int cellIndex, string text)
   {
      TableCell cell = row.Cells[cellIndex];
      GameObject go = Instantiate(textPrefab, cell.transform);
      TextMeshProUGUI textGo = go.GetComponentInChildren<TextMeshProUGUI>();
      textGo.text = text;
   }

   private void InstantiateJoin(ServerData server, TableRow row, int cellIndex)
   {
      TableCell cell5 = row.Cells[5];
      GameObject go = Instantiate(joinButtonPrefab,cell5.transform);
      Button button = go.GetComponentInChildren<Button>();
      button.onClick.AddListener(delegate() {joinController.OnOpenModal(server);});
   }
}
